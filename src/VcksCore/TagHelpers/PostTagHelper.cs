using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

using VcksCore.BLL.DTO;

namespace VcksCore.TagHelpers
{
    public class PostTagHelper : TagHelper
    {
        public int userId { get; set; }
        public WallPostDTO wp { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("id", $"post_{wp.Id}");
            output.Attributes.SetAttribute("class", "WallPost myShadow");

            string content = $"<table class='WallPostTable'><tr><td width='105' valign='top' rowspan='3'><div style='margin:5px'><a href='/Profile/{wp.From.Id}'><img class='WallPostUserPhoto myShadow' src='/File/{wp.From.Avatar.Square_100Id}'/></a></div></td><td align='left'><div class='WallPostUserName'><a href='/Profile/{wp.FromId}' class='DefaultLink'>{wp.From.FirstName} {wp.From.LastName}</a></div></td><td align='right'><div class='WallPostDate text-muted'>{wp.Date.ToString()}</div></td></tr><tr><td colspan='2'><div class='WallPostText'>{wp.Text}</div></td></tr><tr><td colspan='2' align='right'><div class='WallPostActions'>";

            if (wp.FromId.Equals(userId))
                content += $"<a href='#vcks' onclick='WallPostEdit({wp.Id})' class='DefaultLink'>Edit</a>";

            if (wp.FromId.Equals(userId) || wp.OwnerId.Equals(userId))
                content += $"&nbsp;<a href='#vcks' onclick='WallPostDelete({wp.Id})' class='DefaultLink'>Delete</a>";

            content += "</div></td></tr></table>";

            output.Content.SetHtmlContent(content);
        }
    }
}
