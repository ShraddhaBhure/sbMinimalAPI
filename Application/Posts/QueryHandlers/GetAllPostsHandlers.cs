﻿using Application.Abstractions;
using Application.Posts.Queries;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.QueryHandlers
{
    public class GetAllPostsHandlers : IRequestHandler<GetAllPosts, ICollection<Post>>
    {
        private readonly IPostRepository _postRepo;
        public GetAllPostsHandlers(IPostRepository postRepo)
        {
            _postRepo = postRepo;


        }
        public async Task<ICollection<Post>> Handle(GetAllPosts request, CancellationToken cancellationToken)
        {
            return await _postRepo.GetAllPosts();
        }
    }
}
