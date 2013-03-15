<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="html" indent="yes"/>

    <xsl:template match="/">
      <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
      <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
        <head>
          <meta charset="utf-8" />          
        </head>
        <body>
          <h3>Welcome to Runner's Tracker!</h3>
          <p>Dear <xsl:value-of select="/welcomeEmail/firstName"/>:
        </p>
          <p>
            Thank you for signing up!  Please click 
            <xsl:variable name="link" select="/welcomeEmail/confirmLink" />            
            <a href="{$link}">here</a> to activate your account.
          </p>
          <p>If the link doesn't work then copy and paste this link into your browser:</p>
          <p><xsl:value-of select="/welcomeEmail/confirmLink"/></p>
        </body>
      </html>
    </xsl:template>
</xsl:stylesheet>
