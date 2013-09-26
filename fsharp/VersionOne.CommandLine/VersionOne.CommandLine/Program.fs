module VersionOne.CommandLine

open OAuth2Client

open ArgParse

let await = Async.AwaitTask
let start = Async.StartAsTask

type Options = {
  url     : string
  creds   : string
  secrets : string
  scope   : string
  endpoint : string
  quiet : bool
  outputs : string list
  errors  : string list
  }

let optionDef = {
  Default = { url     = "https://www14.v1host.com/v1sdktesting"
              creds   = "stored_credentials.json"
              secrets = "client_secrets.json"
              scope   = "query-api-1.0"
              endpoint = "/query.v1"
              quiet = false
              outputs = []
              errors  = [] }
  Switches = Map [ "-h",       fun   o -> failwith "Would display help here"
                   "-q",       fun   o -> {o with quiet = true} ]
  Values = Map [ "--url",      fun v o -> {o with url = v}
                 "--scope",    fun v o -> {o with scope = v}
                 "--endpoint", fun v o -> {o with endpoint = v}
                 "--creds",    fun v o -> {o with creds = v}
                 "--secrets",  fun v o -> {o with secrets = v} ]
  Other =                      fun v o -> {o with outputs = v::o.outputs}
  Error =                      fun v o -> printfn "WARNING: Ignored argument: %s" v
                                          {o with errors = v::o.errors}
  }

open System
open System.Net
open System.Net.Http

let mainAsync argv = start <| async {
  let opts = optionDef.ofArray argv
  let storage = Storage.JsonFileStorage(opts.secrets, opts.creds)
  let client = HttpClient.WithOAuth2(opts.scope, storage)
  let endpoint = opts.url.TrimEnd([|'/'|]) + "/" + opts.endpoint.TrimStart([|'/'|])
  if not opts.quiet then
    eprintfn "Will make query to %s" endpoint
    eprintfn "Enter the query.  Query will be sent at end of input."
  let! input = await <| Console.In.ReadToEndAsync()
  use content = new StringContent(input)
  let! response = await <| client.PostAsync(endpoint, content)
  if response.StatusCode <> HttpStatusCode.OK then
    if not opts.quiet then
      eprintfn "An error occurred.\n%A" response
    return 1
  else
    let! body = await <| response.Content.ReadAsStringAsync()
    let! success = Async.AwaitIAsyncResult <| Console.Out.WriteAsync(body)
    return 0
  }


open Nito.AsyncEx.Synchronous

[<EntryPoint>]
let main argv = (mainAsync argv).WaitAndUnwrapException()

