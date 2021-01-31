using AutoMapper;
using DBI_Apotheke.Core.Util;
using Xunit;

namespace DBI_Apotheke.Test
{
    public sealed class MapperTests
    {
        [Fact]
        public void TestMappingProfile()
        {
            var profile = new MapperProfile();
            var cfg = new MapperConfiguration(c => c.AddProfile(profile));
            cfg.AssertConfigurationIsValid();
        }
    }
}