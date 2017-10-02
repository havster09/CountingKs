﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CountingKs.Data;
using CountingKs.Services;
using System.Net.Http;
using System.Net;

namespace CountingKs.Controllers
{
    public class DiarySummaryController : BaseApiController
    {
        private ICountingKsIdentityService _identityService;

        public DiarySummaryController(ICountingKsRepository repo, ICountingKsIdentityService identityService) : base(repo)
        {
            _identityService = identityService;
        }

        public object Get(DateTime diaryId)
        {
            try
            {
                var diary = TheRepository.GetDiary(_identityService.CurrentUser, diaryId);
                if(diary == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                return TheModelFactory.CreateSummary(diary);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}