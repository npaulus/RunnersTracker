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
          <h3>Runner's Tracker Password Reset</h3>
          <p>Dear <xsl:value-of select="/password/firstName"/>:
        </p>
          <p>
            Your password has been reset.  Please click 
            <xsl:variable name="link" select="/password/resetLink" />            
            <a href="{$link}">here</a> to set your new password.
          </p>
          <p>If the link doesn't work then copy and paste this link into your browser:</p>
          <p><xsl:value-of select="/password/resetLink"/></p>
        </body>
      </html>
    </xsl:template>
</xsl:stylesheet>
