﻿using System;
using System.Collections.Generic;
using FacebookToOutlookCore.Model.Interfaces;
using FacebookToOutlookCore.Repositories.Interfaces;
using VSTOContrib.Outlook.Services;

namespace FacebookToOutlookCore.Services
{
    public class OutlookRepositorySyncProviderAdapter : ISynchronisationProvider<IFacebookEvent, long>
    {
        private readonly IOutlookRepository _outlookRepository;

        public OutlookRepositorySyncProviderAdapter(IOutlookRepository outlookRepository)
        {
            _outlookRepository = outlookRepository;
        }

        public IEnumerable<IFacebookEvent> GetModifiedEntries(DateTime? lastSync)
        {
            return
                (lastSync == null
                     ? _outlookRepository.GetEvents()
                     : _outlookRepository.GetModifiedEvents(lastSync.Value));
        }

        public IEnumerable<long> GetDeletedEntries(DateTime? lastSync)
        {
            return _outlookRepository.GetDeletedEventIds();
        }

        public void SaveEntries(IEnumerable<IFacebookEvent> entries)
        {
            _outlookRepository.SaveEvents(entries);
        }

        public void DeleteEntries(IEnumerable<long> keys)
        {
            foreach (var key in keys)
            {
                _outlookRepository.DeleteEvent(key);
            }
        }
    }
}