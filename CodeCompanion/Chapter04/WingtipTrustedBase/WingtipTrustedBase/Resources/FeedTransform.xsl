<xsl:stylesheet version="1.0"
  xmlns:atom="http://www.w3.org/2005/Atom"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:d="http://schemas.microsoft.com/ado/2007/08/dataservices"
  xmlns:m="http://schemas.microsoft.com/ado/2007/08/dataservices/metadata" >

  <xsl:output method="html" indent="yes" omit-xml-declaration="yes" />

  <xsl:template match="/atom:feed">
    <table border="1px solid black" cellpadding="4px">
      <tr style="color:white;background-color:black">
        <td>Last Name</td>
        <td>First Name</td>
        <td>Company</td>
        <td>Phone</td>
      </tr>
      <xsl:apply-templates select="atom:entry" />
    </table>
  </xsl:template>

  <xsl:template match="atom:entry" name="feed">
    <tr>
      <td>
        <xsl:value-of select="atom:title" />
      </td>
      <td>
        <xsl:value-of select="atom:content/m:properties/d:FirstName" />
      </td>
      <td>
        <xsl:value-of select="atom:content/m:properties/d:Company" />
      </td>
      <td>
        <xsl:value-of select="atom:content/m:properties/d:BusinessPhone" />
      </td>
    </tr>
  </xsl:template>

</xsl:stylesheet>
