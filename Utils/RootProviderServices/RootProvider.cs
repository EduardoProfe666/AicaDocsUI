using Microsoft.Extensions.Options;

namespace AicaDocsUI.Utils.RootProviderServices;

public class RootProvider
{
    public string RootApi { get; set; }
    public required string RootUI { get; set; }

    public RootProvider(IOptions<RootProviderOptions> options)
    {
        RootApi = options.Value.RootApi;
        RootUI = options.Value.RootUI;
    }
}