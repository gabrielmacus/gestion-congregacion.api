﻿using AutoMapper;
using gestion_congregacion.api.Features.Common;
using gestion_congregacion.api.Features.Operations;
using gestion_congregacion.api.Features.Permissions;
using gestion_congregacion.api.Features.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace gestion_congregacion.api.Features.Publishers
{
    public class PublisherController : CRUDController<Publisher>, IPublisherController
    {
        public PublisherController(
            ICreateOperation<Publisher> createOperation,
            IReadOperation<Publisher> readOperation,
            IUpdateOperation<Publisher> updateOperation,
            IDeleteOperation<Publisher> deleteOperation) : base(readOperation, createOperation, updateOperation, deleteOperation)
        {
        }

        [EnableQuery]
        public override IQueryable<Publisher> Get()
        { 
            return base.Get();
        }

        /*
        [HasPermission("Publishers.Read")]
        public override Task<IActionResult> Get(long key)
        {
            return base.Get(key);
        }*/
    }
}
