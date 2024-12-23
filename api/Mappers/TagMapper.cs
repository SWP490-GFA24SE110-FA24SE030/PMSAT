using api.Dtos.Tag;
using api.Models;

namespace api.Mappers
{
    public static class TagMapper
    {
        public static TagDto ToTagDto(this Tag tagModel)
        {
            return new TagDto
            {
                Id = tagModel.Id,
                Name = tagModel.Name,
                Tasks = tagModel.Tasks,
            };
        }
    }
}
