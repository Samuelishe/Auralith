using System.Reflection;
using Auralith.Core;
using Shouldly;
using Xunit;

namespace Auralith.Core.Tests;

public sealed class CoreAssemblyTests
{
    [Fact]
    public void Core_assembly_loads()
    {
        var assembly = Assembly.Load("Auralith.Core");

        assembly.GetName().Name.ShouldBe("Auralith.Core");
    }

    [Fact]
    public void MediaOpenRequest_accepts_existing_file()
    {
        var file = Path.GetTempFileName();
        try
        {
            var result = MediaOpenRequest.TryCreate(file, out var request, out var error);

            result.ShouldBeTrue();
            request.ShouldNotBeNull();
            request.Path.ShouldBe(Path.GetFullPath(file));
            error.ShouldBeNull();
        }
        finally
        {
            File.Delete(file);
        }
    }

    [Fact]
    public void MediaOpenRequest_rejects_missing_file()
    {
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".mp4");

        var result = MediaOpenRequest.TryCreate(path, out var request, out var error);

        result.ShouldBeFalse();
        request.ShouldBeNull();
        error.ShouldBe("Media file does not exist.");
    }

    [Fact]
    public void MediaOpenRequest_rejects_empty_path()
    {
        var result = MediaOpenRequest.TryCreate(" ", out var request, out var error);

        result.ShouldBeFalse();
        request.ShouldBeNull();
        error.ShouldBe("No media file path was provided.");
    }

    [Fact]
    public void MediaOpenRequest_rejects_folder()
    {
        var result = MediaOpenRequest.TryCreate(Path.GetTempPath(), out var request, out var error);

        result.ShouldBeFalse();
        request.ShouldBeNull();
        error.ShouldBe("Folder opening is not supported in this spike.");
    }

    [Fact]
    public void MediaOpenRequest_uses_first_non_option_command_line_argument()
    {
        var file = Path.GetTempFileName();
        try
        {
            var result = MediaOpenRequest.TryCreateFromCommandLine(["--ignored", file], out var request, out var error);

            result.ShouldBeTrue();
            request.ShouldNotBeNull();
            request.Path.ShouldBe(Path.GetFullPath(file));
            error.ShouldBeNull();
        }
        finally
        {
            File.Delete(file);
        }
    }
}
