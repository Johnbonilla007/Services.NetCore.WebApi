using System;
using WebServices.NetCore.Criostasis.AplicationServices.Core.DTOs;

namespace Services.NetCore.WebApi.Domain.Core
{
    public static class TransactionInfoFactory
    {
        public static TransactionInfo CrearTransactionInfo(UserInfoDto userInfoDTO, string transaccionId)
        {
            //ValidarArgumentosUserInfo(userInfoDTO);

            TransactionInfo transactionInfo = new TransactionInfo(userInfoDTO.CreatedBy, transaccionId);

            transactionInfo.GenerateTransactions = true;

            return transactionInfo;
        }

        internal static object CrearTransactionInfo(UserInfoDto requestUserInfo, object iMPORT_INVOICE_FROM_CTS)
        {
            throw new NotImplementedException();
        }
    }
}
