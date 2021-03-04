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
          <h2>Solicitud</h2>
          Número: <xsl:value-of select="/solicitud/numero"/>
          <br />
          Fecha de entrega: <xsl:value-of select="/solicitud/fechaEntrega"/>
          <br />
          Nombre destinatario: <xsl:value-of select="/solicitud/nombreDestinatario"/>
          <br />
          Dirección: <xsl:value-of select="/solicitud/direccionDestinatario"/>
          <br />
          Estado: <xsl:value-of select="/solicitud/estado"/>
          <br />
          <xsl:for-each select="/solicitud/paquetesSolicitud/paquete">
            <h3>Paquete código <xsl:value-of select="codigo"/></h3>
            Tipo: <xsl:value-of select="tipo"/>
            <br />
            Descripción: <xsl:value-of select="descripcion"/>
            <br />
            Peso: <xsl:value-of select="peso"/> grs.
            <br />

            <h4>Empresa origen</h4>
            Usuario: <xsl:value-of select="empresa/logueo"/>
            <br />
            Nombre: <xsl:value-of select="empresa/nombreCompleto"/>
            <br />
            Teléfono: <xsl:value-of select="empresa/telefono"/>
            <br />
            Dirección: <xsl:value-of select="empresa/direccion"/>
            <br />
            Email: <xsl:value-of select="empresa/email"/>
            <br />
          </xsl:for-each>
        </body>
      </html>
    </xsl:template>
</xsl:stylesheet>
