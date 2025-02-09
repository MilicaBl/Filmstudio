using System;
using System.Security.Claims;

namespace API;

public class FilmStudioHelpers
{
        public static int? GetUserFilmStudioId(ClaimsPrincipal user)
        {
            var filmStudioClaim = user.FindFirst("FilmStudioId")?.Value;
            if (int.TryParse(filmStudioClaim, out int parsedId))
            {
                return parsedId;
            }
            return null;
        }
}
