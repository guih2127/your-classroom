select * from AspNetUsers;

-- Obtém todos os users e seus roles
select us.UserName, us.Email, r.Name
from AspNetUserRoles u
join AspNetRoles r on u.RoleId = r.Id
join AspNetUsers us on u.UserId = us.Id;