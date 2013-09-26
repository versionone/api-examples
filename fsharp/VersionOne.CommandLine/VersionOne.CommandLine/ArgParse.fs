module ArgParse

///
/// Command line option parsing library
///
/// Create an OptParse<'Options> object when you 
///
type OptParse<'Options> = {
    Default   : 'Options
    Switches  : Map<string, 'Options -> 'Options>
    Values    : Map<string, string -> 'Options -> 'Options>
    Other     : (string -> 'Options -> 'Options)
    Error     : (string -> 'Options -> 'Options)
  } with

  member x.isSwitch = x.Switches.ContainsKey

  member x.isValue = x.Values.ContainsKey

  member x.splitEq (s:string) =
    match s.Split([|'='|], 2) with
    | [| s |] -> false, s, ""
    | [| k; v |] -> (x.isValue k), k, v

  member x.isEquals s = let p,_,_ = x.splitEq s in p

  member x.handleEq e = 
    let p,f,v = x.splitEq e
    if p then x.Values.[f] v else x.Error f

  member x.ofArray (argv:string[]) =
    let rec parse args opts =
      match args with
      |      f :: more when x.isSwitch f      -> opts |> x.Switches.[f]  |> parse more
      |      e :: more when x.isEquals e      -> opts |> x.handleEq e    |> parse more
      |      f :: []   when x.isValue f       -> opts |> x.Error f       |> parse [] 
      | f :: v :: more when x.isValue  f      -> opts |> x.Values.[f] v  |> parse more
      |      d :: more when d.StartsWith("-") -> opts |> x.Error d       |> parse more
      |      o :: more                        -> opts |> x.Other o       |> parse more
      |             []                        -> opts
    x.Default |> parse (List.ofArray argv)
