from reportlab.pdfgen import canvas
from reportlab.graphics.shapes import Drawing 
from reportlab.graphics.barcode.qr import QrCodeWidget 
from reportlab.graphics import renderPDF


p = canvas.Canvas("somefile.pdf", pagesize=letter)
qrw = QrCodeWidget('https://www14.v1host.com/v1sdktesting/AssetDetail.mvc?Oid=Story:1004') 
b = qrw.getBounds()

w=b[2]-b[0] 
h=b[3]-b[1] 

d = Drawing(45, 45, transform=[45./w,0,0,45./h,0,0]) 
d.add(qrw)

renderPDF.draw(d, p, 1, 1)

p.showPage()
p.save()