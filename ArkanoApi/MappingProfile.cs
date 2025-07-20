//-----------------------------------------------------------------------
// <copyright file="MappingProfile" company="Chubb">
//     All rights reserved.
// </copyright>
// <author>aifre</author>
// <date>18/07/2025 20:57:14</date>
// <summary>Código fuente interfaz MappingProfile.</summary>
//-----------------------------------------------------------------------

namespace ArkanoApi
{
    using ArkanoBussiness.Dtos;
    using ArkanoData.Entities;
    using AutoMapper;

    /// <summary>
    /// MappingProfile.
    /// </summary>
	internal class MappingProfile : Profile
    {
        #region Attributes

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Transaction, TransactionRequest>().ReverseMap();
            CreateMap<Transaction, TransactionResponse>().ReverseMap();
        }

        #endregion

        #region Properties

        #endregion

        #region Methods And Functions

        #endregion
    }
}
