using RadiusDomain.Entities;

namespace RadiusDomain.ValueObjects;

public class RadGroupAttributeCatalog
{
    public readonly RadGroup[] RadGroupAttributes = new[]
    {
        new RadGroup("GATTR_001", new[] { "Cleartext-Password", "md5" }),
        new RadGroup("GATTR_002", new[] { "Framed-IP-Address" }),
    };
}