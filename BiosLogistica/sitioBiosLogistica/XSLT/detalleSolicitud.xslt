<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:output method="html" indent="yes"/>

    <xsl:template match="/">
      <html>
        <head>
          <title>Detalle Tramite</title>
        </head>
        <body>
          <h3>Solicitud</h3>
          Número: <xsl:value-of select="/Solicitud/numero"/>
          <br />
          Fecha de entrega: <xsl:value-of select="/Solicitud/numero"/>
          <br />
          Nombre destinatario: <xsl:value-of select="/Solicitud/numero"/>
          <br />
          Dirección: <xsl:value-of select="/Solicitud/numero"/>
          <br />
          Estado: <xsl:value-of select="/Solicitud/numero"/>
          <br />
          <h3>Paquetes</h3>

          <xsl:for-each select="/Solicitud/paquetesSolicitud/paquete">
            
          </xsl:for-each>
        </body>
      </html>
    </xsl:template>
</xsl:stylesheet>
