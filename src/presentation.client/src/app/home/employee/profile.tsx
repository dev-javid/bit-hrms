import { useGetEmployeeQuery } from '@/lib/rtk/rtk.employees';
import useAuth from '@/lib/hooks/use-auth';
import { Card, CardHeader, CardTitle, CardDescription, CardContent, Separator, Badge, Container } from 'xplorer-ui';

const Profile = () => {
  const { user } = useAuth();
  const { data, isFetching } = useGetEmployeeQuery({
    employeeId: user.employeeId,
  });

  return (
    <Container isLoading={isFetching}>
      {data && (
        <div>
          <Card className="overflow-hidden" x-chunk="dashboard-05-chunk-4">
            <CardHeader className="flex flex-row items-start">
              <div className="grid gap-0.5">
                <CardTitle className="text-4xl">{data.fullName}</CardTitle>
                <CardDescription>Since: {data.dateOfJoining.asDateOnly().toDayString()}</CardDescription>
              </div>
              <div className="ml-auto items-center gap-1">
                <Badge variant="outline"> {data.jobTitleName}</Badge>
              </div>
            </CardHeader>
            <CardContent className="p-6 text-sm">
              <div className="grid gap-3">
                <div className="font-semibold">Basic Details</div>
                <ul className="grid gap-3">
                  <li className="flex items-center justify-between">
                    <span className="text-muted-foreground">First Name</span>
                    <span>{data.firstName}</span>
                  </li>
                  <li className="flex items-center justify-between">
                    <span className="text-muted-foreground">Last Name</span>
                    <span>{data.lastName}</span>
                  </li>
                  <li className="flex items-center justify-between">
                    <span className="text-muted-foreground">Father's Name</span>
                    <span>{data.fatherName}</span>
                  </li>
                  <li className="flex items-center justify-between">
                    <span className="text-muted-foreground">D.O.B</span>
                    <span>{data.dateOfBirth.asDateOnly().toDayString()}</span>
                  </li>
                  <li className="flex items-center justify-between">
                    <span className="text-muted-foreground">Department</span>
                    <span>{data.departmentName}</span>
                  </li>
                </ul>
                <Separator className="my-2" />
                <div className="font-semibold">Contact Details</div>
                <ul className="grid gap-3">
                  <li className="flex items-center justify-between">
                    <span className="text-muted-foreground">Company Email</span>
                    <span>{data.companyEmail}</span>
                  </li>
                  <li className="flex items-center justify-between">
                    <span className="text-muted-foreground">Personal Email</span>
                    <span>{data.personalEmail}</span>
                  </li>
                  <li className="flex items-center justify-between">
                    <span className="text-muted-foreground">Phone Number</span>
                    <span>{data.phoneNumber}</span>
                  </li>
                </ul>
              </div>
              <Separator className="my-4" />
              <div className="grid grid-cols-2 gap-4">
                <div className="grid gap-3">
                  <div className="font-semibold">Address</div>
                  <address className="grid gap-0.5 not-italic text-muted-foreground">
                    <span>{data.address}</span>
                  </address>
                </div>
                <div className="grid auto-rows-max gap-3">
                  <div className="font-semibold">City</div>
                  <div className="text-muted-foreground">{data.city}</div>
                </div>
              </div>
            </CardContent>
          </Card>
        </div>
      )}
    </Container>
  );
};

export default Profile;
