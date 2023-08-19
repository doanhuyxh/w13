using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTEcommerce.Web
{
    public class EnumHSO
    {
        public enum EnumSectHSO
        {
            chien_binh = 1,
            sat_thu = 2,
            phap_su = 3,
            xa_thu = 4
        }
        public enum EnumServerHSO
        {
            chien_than = 1,
            rong_lua = 2,
            phuong_hoang = 3,
            nhan_ma = 4
        }
        public enum EnumStatusHSO
        {
            cho_duyet = 1,
            da_duyet = 2,
            da_ban = 3,
            sai_tk_mk = 4,
            da_huy_dang_ky_that = 5,
            da_huy_dinh_mien = 6,
            da_huy_khong_doi_mk = 7
        }
    }
}