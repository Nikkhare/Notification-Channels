using Microsoft.AspNetCore.Mvc;
using Notification.Models;
using System.Text.RegularExpressions;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    [HttpPost]
    public IActionResult ParseChannels([FromBody] NotificationViewModel request)
    {
        if (request == null || string.IsNullOrEmpty(request.Title))
        {
            return BadRequest("Title is required.");
        }

        try
        {
            var channels = ExtractChannels(request.Title);
            return Ok(new { channels });
        }
        catch
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

    private static List<string> ExtractChannels(string title)
    {
        Regex regex = new Regex(@"\[([^\]]+)\]");
        MatchCollection matches = regex.Matches(title);
        List<string> channels = new List<string>();

        foreach (Match match in matches)
        {
            var tag = match.Groups[1].Value;
            if (IsValidChannel(tag)) // Check if the tag is a valid channel
            {
                channels.Add(tag);
            }
        }

        return channels;
    }

    private static bool IsValidChannel(string tag)
    {
        var validChannels = new[] { "BE", "FE", "QA", "Urgent" };
        return validChannels.Contains(tag);
    }
}
