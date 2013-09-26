

import html_template

import v1pysdk
import pprint
def getCards():
    with v1pysdk.V1Meta(instance_url="https://www14.v1host.com/v1sdktesting", username="admin", password="admin") as v1:
        tasks = (v1.Task
                   .filter("Parent.Scope.Name='Sample: Release 1.0'")
                   .select("Name", "Description", "ToDo", "Estimate", "Owners.Name", "Status.Name")
                   )
        for row in tasks:
            yield {
                "title": row.Name,
                "description": row.Description,
                "todo": row.ToDo,
                "done": row.Estimate,
                "priority": row.Status.Name,
                "owners": [o.Name for o in row.Owners],
                "qrdata": ""
            }


def items():
    yield html_template.head()
    for n, row in enumerate(getCards()):
        onleft = (n % 2 == 0)
        row["onleft"] = onleft
        yield html_template.card(**row)
    yield html_template.tail()

def render():
    return "\n".join(items())

def main():
    html_out = render()
    print html_out


if __name__ == "__main__":
    main()

