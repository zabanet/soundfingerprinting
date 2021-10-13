namespace SoundFingerprinting.Configuration
{
    using System;
    using System.Collections.Generic;
    using SoundFingerprinting.Command;
    using SoundFingerprinting.Data;
    using SoundFingerprinting.Query;
    using SoundFingerprinting.Strides;

    /// <summary>
    ///   Configuration options used when querying the data source in realtime.
    /// </summary>
    public class RealtimeQueryConfiguration
    {
        public RealtimeQueryConfiguration(int thresholdVotes,
            IRealtimeResultEntryFilter resultEntryFilter,
            Action<QueryResult> successCallback,
            Action<QueryResult> didNotPassFilterCallback,
            IRealtimeResultEntryFilter ongoingResultEntryFilter,
            Action<ResultEntry> ongoingSuccessCallback,
            Action<Exception, Hashes> errorCallback,
            Action restoredAfterErrorCallback,
            IOfflineStorage offlineStorage,
            IStride stride,
            double permittedGap,
            double downtimeCapturePeriod,
            IDictionary<string, string> yesMetaFieldFilters,
            IDictionary<string, string> noMetaFieldsFilters)
        {
            QueryConfiguration = new DefaultQueryConfiguration
            {
                ThresholdVotes = thresholdVotes,
                FingerprintConfiguration = new DefaultFingerprintConfiguration
                {
                    SpectrogramConfig = new DefaultSpectrogramConfig
                    {
                        Stride = stride
                    }
                },
                YesMetaFieldsFilters = yesMetaFieldFilters,
                NoMetaFieldsFilters = noMetaFieldsFilters,
                PermittedGap = permittedGap
            };
                
            ResultEntryFilter = resultEntryFilter;
            SuccessCallback = successCallback;
            DidNotPassFilterCallback = didNotPassFilterCallback;
            OngoingResultEntryFilter = ongoingResultEntryFilter;
            OngoingSuccessCallback = ongoingSuccessCallback;
            ErrorCallback = errorCallback;
            RestoredAfterErrorCallback = restoredAfterErrorCallback;
            OfflineStorage = offlineStorage;
            DowntimeCapturePeriod = downtimeCapturePeriod;
        }

        /// <summary>
        ///   Gets or sets vote count for a track to be considered a potential match (i.e. [1; 25]).
        /// </summary>
        public int ThresholdVotes
        {
            get => QueryConfiguration.ThresholdVotes;
            set => QueryConfiguration.ThresholdVotes = value;
        }

        /// <summary>
        ///  Gets or sets result entry filter.
        /// </summary>
        public IRealtimeResultEntryFilter ResultEntryFilter { get; set; }

        /// <summary>
        ///   Gets or sets success callback invoked when a candidate passes result entry filter.
        /// </summary>
        public Action<QueryResult> SuccessCallback { get; set; }
        
        /// <summary>
        ///  Gets or sets callback invoked when a candidate did not pass result entry filter, but has been considered a candidate.
        /// </summary>
        public Action<QueryResult> DidNotPassFilterCallback { get; set; }

        /// <summary>
        ///  Gets or sets ongoing result entry filter that will be invoked for every result entry filter that is captured by the aggregator.
        /// </summary>
        /// <remarks>
        ///  Experimental, may change in the future.
        /// </remarks>
        public IRealtimeResultEntryFilter OngoingResultEntryFilter { get; set; }
        
        /// <summary>
        ///  Gets or sets ongoing success callback that will be invoked once ongoing result entry filter is passed.
        /// </summary>
        /// <remarks>
        ///  Experimental, may change in the future.
        /// </remarks>
        public Action<ResultEntry> OngoingSuccessCallback { get; set; }

        /// <summary>
        ///  Gets or sets error callback which will be invoked in case if realtime command fails to execute.
        /// </summary>
        public Action<Exception, Hashes> ErrorCallback { get; set; }

        /// <summary>
        ///  Gets or sets error restore callback.
        /// </summary>
        public Action RestoredAfterErrorCallback { get; set; }

        /// <summary>
        ///  Gets or sets offline storage for hashes.
        /// </summary>
        /// <remarks>
        ///  Experimental, can be used to store hashes during the time the storage is not available. Will be consumed, after RestoredAfterErrorCallback invocation.
        /// </remarks>
        public IOfflineStorage OfflineStorage { get; set; }

        /// <summary>
        ///  Gets or sets downtime capture period (in seconds), value which will instruct the framework to cache realtime hashes for later processing in case if an unsuccessful request is made to Emy.
        /// </summary>
        /// <remarks>
        ///  Experimental.
        /// </remarks>
        public double DowntimeCapturePeriod { get; set; }

        /// <summary>
        ///  Gets or sets stride between 2 consecutive fingerprints used during querying.
        /// </summary>
        public IStride Stride
        {
            get => QueryConfiguration.Stride;
            set => QueryConfiguration.Stride = value;
        }

        /// <summary>
        ///  Gets or sets permitted gap between consecutive candidate so that they are glued together.
        /// </summary>
        public double PermittedGap
        {
            get => QueryConfiguration.PermittedGap;
            set => QueryConfiguration.PermittedGap = value;
        }

        /// <summary>
        ///  Gets or sets list of positive meta fields to consider when querying the data source for potential candidates.
        /// </summary>
        public IDictionary<string, string> YesMetaFieldsFilter
        {
            get => QueryConfiguration.YesMetaFieldsFilters;
            set => QueryConfiguration.YesMetaFieldsFilters = value;
        }

        /// <summary>
        ///  Gets or sets list of negative meta fields to consider when querying the data source for potential candidates.
        /// </summary>
        public IDictionary<string, string> NoMetaFieldsFilter
        {
            get => QueryConfiguration.NoMetaFieldsFilters;
            set => QueryConfiguration.NoMetaFieldsFilters = value;
        }

        internal QueryConfiguration QueryConfiguration { get; }
    }
}