

import html_template

def getCards():
	for row in csv:
		yield {
			title: "",
			description: "",
			todo: "",
			done: "",
			priority: "",
			owners: "",
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

