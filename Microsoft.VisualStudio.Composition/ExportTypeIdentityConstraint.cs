﻿namespace Microsoft.VisualStudio.Composition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Validation;

    public class ExportTypeIdentityConstraint : IImportSatisfiabilityConstraint
    {
        private readonly string typeIdentityName;

        public ExportTypeIdentityConstraint(Type typeIdentity)
        {
            Requires.NotNull(typeIdentity, "typeIdentity");
            this.typeIdentityName = ContractNameServices.GetTypeIdentity(typeIdentity);
        }

        public ExportTypeIdentityConstraint(string typeIdentityName)
        {
            Requires.NotNullOrEmpty(typeIdentityName, "typeIdentityName");
            this.typeIdentityName = typeIdentityName;
        }

        public bool IsSatisfiedBy(ExportDefinition exportDefinition)
        {
            Requires.NotNull(exportDefinition, "exportDefinition");

            object value;
            if (exportDefinition.Metadata.TryGetValue(CompositionConstants.ExportTypeIdentityMetadataName, out value))
            {
                return this.typeIdentityName == value as string;
            }

            return false;
        }
    }
}
