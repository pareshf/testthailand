<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" xmlns:User="ext:PosswordRecovery">
	<xsl:template match="/">
		<html>
			<body>
				<p>
					Dear <xsl:value-of select ="User:get_UserName()"/>,
				</p>
				<p>
					Your new password is <xsl:value-of select ="User:get_Password()"/>
				</p>
				<p>
					Regards,
					Flamingo Administrator
				</p>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>