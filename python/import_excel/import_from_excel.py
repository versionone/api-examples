import os

from v1pysdk import V1Meta

URL="https://www14.v1host.com/v1sdktesting"
USERNAME="admin"
PASSWORD="admin"


def get_excel_data(filename):
    from win32com import client
    print "Opening workbook %s"%(filename,)
    excel = client.Dispatch("Excel.Application")
    # excel.Visible=True
    book = excel.Workbooks.Open(filename, False, True)
    sheet = book.Worksheets(1)
    data = sheet.Range("A2:B3").Value
    print "Got %d rows from worksheet. Exiting excel." %(len(data),)
    excel.Quit()
    return data


with V1Meta(instance_url=URL, username=USERNAME, password=PASSWORD) as v1:
    filename = os.path.abspath("test.xlsx")
    for storyNum, newEstimate in get_excel_data(filename):
        b = v1.Story.where(Number=storyNum).first()
        print "Found: ", b, "updating estimate..."
        b.Estimate = newEstimate

print "Updates saved"
