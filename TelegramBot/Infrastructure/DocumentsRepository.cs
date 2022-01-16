using System;
using System.Collections.Generic;

namespace TelegramBot.Infrastructure
{
    public static class DocumentsRepository
    {
        public static readonly Dictionary<string, string> DocumentsWithUrl = new Dictionary<string, string>()
        {
            { "Все документы", "https://urfu.ru/ru/students/documents/"},
            { "Об обязательной вакцинации", "https://urfu.ru/fileadmin/user_upload/common_files/employee/docs/Prikaz_po_osnovnoi___dejatelnosti_No_0657_03_ot_25.08.2021__O_nachale_2021_2....pdf" },
            { "О начале 2021/2022 учебного года", "https://urfu.ru/fileadmin/user_upload/common_files/employee/docs/Prikaz_po_osnovnoi___dejatelnosti_No_0657_03_ot_25.08.2021__O_nachale_2021_2....pdf" },
            { "Справка о переводе", "https://urfu.ru/fileadmin/user_upload/urfu.ru/documents/education/2017/201702_Spravka_o_perevode.pdf" },
            { "О порядке перевода с платного обучения на бесплатное", "https://urfu.ru/fileadmin/user_upload/urfu.ru/documents/education/2016/No_SMK-PVD-7.5.1-01-95-2016_Polozhenie_o_perekhode_s_platnogo_obuchenija_na_besplatnoe__888524_v1_.pdf" },
            { "Положение о стипендиальном обеспечении", "https://urfu.ru/fileadmin/user_upload/urfu.ru/documents/education/stipendii/2017/Polozhenie_o_stipendialnom_obespechenii_obuchajushchikhsja.pdf" },
            { "О размере стипендии для студентов УрФУ", "https://urfu.ru/fileadmin/user_upload/urfu.ru/documents/education/stipendii/2021/Prikaz_No0643_03_ot_18.08.2021_O_razmere_stipendii_dlja_studentov_UrFU.PDF" },
            { "О размере стипендии для аспирантов УрФУ", "https://urfu.ru/fileadmin/user_upload/urfu.ru/documents/education/stipendii/2021/Prikaz_No0642_03_ot_18.08.2021_O_razmere_stipendii_dlja_aspirantov_UrFU.PDF" },
            { "О ликвидации академических задолженностей", "https://urfu.ru/fileadmin/user_upload/urfu.ru/documents/brs/20200114_Polozhenie_o_likvidacii_akadem_zadolzhennostei_v2.pdf" },
            { "О порядке предоставления каникул", "https://urfu.ru/fileadmin/user_upload/urfu.ru/documents/education/2019/Prikaz_po_osnovnoi_dejatelnosti_No_0665_03_ot_01.08.2019__O_vvedenii_v_deistvie_Reglamenta___1785543_v2_.pdf" }
        };
    }
}