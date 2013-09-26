

from setuptools import setup, find_packages

setup(
  name = "VersionOne.Example.PDFCards",
  version = "0.1",  
  description = "Example of rendering cards with QR codes using data from the VersionOne api",  
  author = "Joe Koberg (VersionOne, Inc.)",
  author_email = "Joe.Koberg@versionone.com",
  license = "MIT/BSD",
  keywords = "versionone v1 api sdk oauth2 pdf python query cards",
  url = "http://github.com/VersionOne/api-examples/python/render_cards",
  
  packages = [
    'v1_render_cards',
    ],  
  
  install_requires = [
    'reportlab',
    'oauth2client',
    ],
  )







