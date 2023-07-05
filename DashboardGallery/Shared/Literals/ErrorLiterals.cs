namespace DashboardGallery.Shared.Literals
{
    public class ErrorLiterals
    {
        private I18nText.Text _text;
        public ErrorLiterals(I18nText.Text  text) 
        { 
            _text = text;
        }

        public string Roles_need_almost_one_permission=> _text.RolesNeedAlmostOnePermission;
        public string Rol_name_can_not_be_empty => _text.RolCantBeEmpty;
        public string Image_Format_Not_Allowed => _text.ImageFormatNotAllowed;
        public string You_must_select_one_rol => _text.You_must_select_one_rol;
        public string You_cannot_select_a_role_that_has_already_been_added => _text.You_cannot_select_a_role_that_has_already_been_added;
        public string Pasword_must_be_equals => _text.Password_must_be_equals;
        public string Value_cannot_be_empty => _text.Value_cannot_be_empty;
        public string Pasword_cannot_be_empty => _text.Pasword_cannot_be_empty;
        public string Phone_format_not_valid => _text.Phone_format_not_valid;
        public string Email_format_not_valid => _text.Email_format_not_valid;
        public string YouMustSaveDatasFirst => _text.YouMustSaveDatasFirst;
        public string YouMustSelectValidDate => _text.YouMustSelectValidDate;
    }
}
