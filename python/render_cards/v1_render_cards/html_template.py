def head():
    return """
<html>
    <head>
        <style type="text/css">
            body {
                width: 11in;
                height: 8.5in;
                margin: 0;
                padding: 0;
                font-family: sans-serif;
            }

            div.card{
                width: 5.0in;
                height: 3.75in;
                padding: 0.25in;
                float: left;
                }

            .onleft {
                clear: left;
                }

            div.descriptext {
                width: 100%;
                height: 67%;
                }

            img.qrcode {
                float: left;
                }

            table {
                height: 33%;
                width: 100%;
                border-collapse: collapse;
                }

            td {
                font-weight: bold;
                text-align: center;
                border: 1pt solid grey;
                }

            tr.details td {
                font-size: 12pt;
            }

            tr.owners td {
                font-size: 18pt;
            }

            tr.title td {
                font-size: 36pt;
            }

        </style>
    </head>
    <body>
"""

import urllib

def card(title="", description="", todo="", done="", priority="", owners="", qrdata="", onleft=True, qrsize=100):
    onleft = "onleft" if onleft else ""
    owners = ", ".join(owners)
    qrdata = urllib.urlencode(qrdata)
    return """
        <div class="card %(onleft)s">
            <div class="descriptext"><img class="qrcode" src="https://chart.googleapis.com/chart?cht=qr&chl=%(qrdata)s&chs=%(qrsize)dx%(qrsize)d&choe=UTF-8&chld=L|2">
                %(description)s
            </div>
            <table>
                <tr class="details">
                    <td >ToDo: %(todo)s</td>
                    <td >Done: %(done)s</td>
                    <td >Pri: %(priority)s</td>
                </tr>
                <tr class="owners">
                    <td colspan="3" >Owners: %(owners)s</td>
                </tr>
                <tr class="title">
                    <td colspan="3" >%(title)s</td>
                </tr>
            </table>
        </div>
        """%locals()


def tail():
    return """        
    </body>
</html>
"""