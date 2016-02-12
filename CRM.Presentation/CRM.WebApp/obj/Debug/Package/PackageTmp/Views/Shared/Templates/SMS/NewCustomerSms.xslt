<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" xmlns:User="ext:NewCustomerSms">
  <xsl:template match="/">
    <html>
      <body>
          Dear Customer, Welcome to Flamingo Family. Your Customer ID is <xsl:value-of select ="User:get_customerUniqueId()"/> - You need to use this ID for any transaction with Flamingo. Thanks - www.flamingotravels.co.in
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>