namespace NewDir.Library;

public class UnixFilePermissionTranslator
{
    public static UnixFileMode Parse(string permission)
    {
        if (permission.Length == 4 && int.TryParse(permission, out int result))
        {
            switch (int.Parse(permission))
            {
                case 0:
                    return UnixFileMode.None;
                case 700:
                    return UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute;
                case 770:
                    return UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute &
                           UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute;
                case 777:
                    return UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute &
                           UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute &
                           UnixFileMode.OtherRead & UnixFileMode.OtherWrite & UnixFileMode.OtherExecute;
                case 111:
                    return UnixFileMode.UserExecute;
                
            }
        }   
    }
}