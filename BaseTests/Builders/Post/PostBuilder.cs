using NSIProject.Domain.Entities;
using System;

namespace BaseTests.Builders.Post
{
    public class PostBuilder
    {
        private readonly NSIProject.Domain.Entities.Post _post;

        public PostBuilder()
        {
            _post = new NSIProject.Domain.Entities.Post();
        }

        public PostBuilder WithId(Guid id)
        {
            _post.Id = id;
            return this;
        }

        public PostBuilder WithUser(ApplicationUser user)
        {
            _post.User = user;
            return this;
        }

        public PostBuilder WithTitle(string title)
        {
            _post.Title = title;
            return this;
        }

        public PostBuilder WithContent(string content)
        {
            _post.Content = content;
            return this;
        }

        public NSIProject.Domain.Entities.Post Build()
        {
            return _post;
        }
    }
}