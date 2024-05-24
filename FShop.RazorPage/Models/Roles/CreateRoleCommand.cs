namespace FShop.RazorPage.Models.Roles;

public class CreateRoleCommand
{
    public string Title { get; set; }
    public List<Permission> Permissions { get; set; }
}


public class EditRoleCommand
{
    public long Id { get; set; }
    public string Title { get; set; }
    public List<Permission> Permissions { get; set; }
}

public class RoleDto : BaseDto
{
    public string Title { get; set; }
    public List<Permission> Permissions { get; set; }
}

public enum Permission
{
    Panel_Admin,
    Edit_Profile,
    Change_Password,
    CRUD_Banner,
    CRUD_Slider,
    CRUD_User,
    CRUD_Product,
    Seller_Management,
    Order_Management,
    Role_Management,
    Comment_Management,
    Category_Management,
    Add_Inventory,
    Edit_Inventory,
    User_Management,
    Seller_Panel




}