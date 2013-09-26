

import html_template

import v1pysdk

def getCards():
	with v1pysdk.V1Meta(...) as v1:
		for row in v1.Task.where(...).select("Name", "Description", "Todo", "Done", "Owners.Name"):
			yield {
				title: row.Title,
				description: row.Description,
				todo: row.Todo,
				done: row.Done,
				priority: row.Priority,
				owners: row.data["Owners.Name"],
				qrdata: ""
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

