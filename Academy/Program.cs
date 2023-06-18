using Academy.context;
using Academy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Group = Academy.Models.Group;

AppDbContext appDbContext = new AppDbContext();
START:
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine( "1.Create Group");
Console.WriteLine( "2.Delete Group");
Console.WriteLine( "3. GetAll Group");
Console.WriteLine(  "4.Get Group by Id");
Console.WriteLine(  "5. Create Student");
Console.WriteLine("6.Delete Student");
Console.WriteLine("7. GetAll Student");
Console.WriteLine("8.Get Student by Id");
Console.WriteLine("9. Update Group");
Console.WriteLine("10. Update Student");
Console.Write("Enter: ");
string selectMenu = Console.ReadLine();
switch (selectMenu)
{
    case "1":
        CreateGroup();
        goto START;
    case "2":
        DeleteGroup();
        goto START;
    case "3":
        GetAllGroup();
        goto START;
    case "4":
        GetGroupbyId();
        goto START;
    case "5":
        CreateStudent();
        goto START;
    case "6":
        DeleteStudent();
        goto START;
    case "7":
        GetAllStudent();
        goto START;
    case "8":
        GetStudentbyId();
        goto START;
    case "9":
        UpdateGroup();
        goto START;
    case "10":
        UpdateStudent();
        goto START;
        default:
        Console.WriteLine("Duzgun daxil edin...");
            goto START;
}



void CreateGroup()
{
    Console.Write(  "Enter Group Name: ");
    Group group = new Group
    {

        CreateTime = DateTime.Now,
        Name = Console.ReadLine(),
        
        
    };
    appDbContext.Groups.Add(group);
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Created successfully");
    appDbContext.SaveChanges();
}
void CreateStudent()
{
    Console.Write(  "Enter student Name: ");
    string strName = Console.ReadLine();
    Console.Write("Enter student Surname: ");
    string strSurname = Console.ReadLine();
    Console.Write("Enter student`s group id: ");

    Student student = new Student
    {
        
        Name = strName,
        Surname = strSurname,
        GroupId = int.Parse(Console.ReadLine()),
        CreateTime = DateTime.Now,

    };
    student.group = appDbContext.Groups.Find(student.GroupId);
    appDbContext.Students.Add(student);
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Created successfully");
    appDbContext.SaveChanges();
}
void DeleteGroup()
{
    Console.Write( "Enter id: ");
    int strId = int.Parse(Console.ReadLine());
    Group group = appDbContext.Groups.Find(strId);
    if(group == null)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Duzgun id daxil edilimeyib...");
    }
    else if (group.IsDeleted == true)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Bu group artiq silinib...");
    }
    else
    {
        group.IsDeleted = true;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Removed successfully");
        appDbContext.SaveChanges();
    }

}
void DeleteStudent()
{
    Console.Write("Enter id: ");
    int strId = int.Parse(Console.ReadLine());
    Student student = appDbContext.Students.Find(strId);
    if (student == null)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Duzgun id daxil edilmeyib...");
    }
    else if (student.IsDeleted == true)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Bu student artiq silinib...");
    }
    else
    {
        student.IsDeleted = true;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Removed successfully");
        appDbContext.SaveChanges();
    }
    
}
void GetAllGroup()
{
    IQueryable<Group> query = appDbContext.Groups.AsNoTracking();
    Console.Clear();
    foreach (Group group in query)
    {
        if (group.IsDeleted == false)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(group.Id + ".  " + group.Name);
        }
    }
}
void GetAllStudent()
{
    IQueryable<Student> query = appDbContext.Students.AsNoTracking();
    Console.Clear();
    foreach (Student student in query)
    {
        if (student.IsDeleted == false)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(student.Id + ".  " + student.Name);
        }
    }
}
void GetGroupbyId()
{
    Console.Write("Enter Id: ");
    int findGroup = int.Parse(Console.ReadLine());
    Group group = appDbContext.Groups.AsNoTracking().FirstOrDefault(x=>x.Id==findGroup);
    if(group == null)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Axtarish uzre netice yoxdur...");
        return;
    }
    else if (group.IsDeleted == true)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine( "Bu qrup silinib...");
        return;
    }
    else
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(group.Id + ". " + group.Name);
    }
}

void GetStudentbyId()
{
    Console.Write("Enter Id: ");
    int findStudent = int.Parse(Console.ReadLine());
    Student student = appDbContext.Students.AsNoTracking().FirstOrDefault(x => x.Id == findStudent);
    if (student == null)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Axtarish uzre netice yoxdur...");
        return;
    }
    else if (student.IsDeleted == true)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Bu student silinib...");
        return;
    }
    else
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(student.Id + ". " + student.Name);
    }
}
void UpdateGroup()
{
    Console.Write("Enter id: ");
    int strId = int.Parse(Console.ReadLine());
    Group group = appDbContext.Groups.Find(strId);
    if (group == null)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Duzgun daxil edin... ");
        return;
    }
    else
    {
        Console.Write("Enter new group name: ");
        string newGroupName = Console.ReadLine();
        group.Name = newGroupName;
        group.UpdateTime = DateTime.Now;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(  "Update Successfully");
        appDbContext.SaveChanges();
    }
}
void UpdateStudent()
{
    Console.Write("Enter id: ");
    int strId = int.Parse(Console.ReadLine());
    Student student = appDbContext.Students.Find(strId);
    if (student == null)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Duzgun daxil edin... ");
        return;
    }
    else
    {
        Console.Write("Enter new student name: ");
        string newStudentName = Console.ReadLine();
        student.Name = newStudentName;
        Console.Write(  "Enter surname: ");
        string newStudentSurname = Console.ReadLine();
        student.Surname = newStudentSurname;
        Console.Write(  "Enter new Group id: ");
        int groupId = int.Parse(Console.ReadLine());
        student.GroupId = groupId;
        student.group = appDbContext.Groups.Find(groupId);
        student.UpdateTime = DateTime.Now;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Update Successfully");
        appDbContext.SaveChanges();
    }
}