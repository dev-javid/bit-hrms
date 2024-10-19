import { BreadCrumbProps, PageContainer } from '@/lib/components';
import { Button, Card, CardContent, CardDescription, CardHeader, CardTitle, Container, Separator, toast } from 'xplorer-ui';
import { useAddSalaryMutation, useGetEstimatedSalaryQuery } from '@/lib/rtk/rtk.salary';
import { useLocation } from 'react-router-dom';
import DeductionList from './deduction-list';
import { DateOnly, Salary } from '@/lib/types';

const EstimatedSalary = () => {
  const { salary } = useLocation().state as { salary: Salary };

  const breadCrumb: BreadCrumbProps = {
    title: 'Salary Management',
    to: './../salary-management',
    child: {
      title: salary.employeeName,
      to: `/app/employees/${salary.employeeId}`,
      child: { title: 'Estimated Salary', to: '' },
    },
  };

  const { data, isLoading, isFetching } = useGetEstimatedSalaryQuery({
    employeeId: salary.employeeId,
    month: salary.month,
    year: salary.year,
  });

  const [addSalary] = useAddSalaryMutation();

  const onPayClick = async () => {
    const response = await addSalary({
      employeeId: salary.employeeId,
      month: salary.month,
      year: salary.year,
    });

    if (!('error' in response)) {
      toast({
        variant: 'primary',
        title: 'Success ',
        description: 'Salary saved successfully.',
      });
    }
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <Container isLoading={isLoading || isFetching}>
        {data && (
          <div className="grid pt-4 md:pt-0 gap-4">
            <Card className="overflow-hidden" x-chunk="dashboard-05-chunk-4">
              <CardHeader className="flex flex-row items-start">
                <div className="grid gap-0.5">
                  <CardTitle className="group flex items-center gap-2 text-lg">{salary.employeeName}</CardTitle>
                  <CardDescription>Salary Breakdown</CardDescription>
                </div>
                <div className="ml-auto flex items-center gap-1">
                  <div className="text-2xl">{DateOnly.fromParts(salary.year, salary.month, 1).toMonthString()}</div>
                </div>
              </CardHeader>
              <CardContent className="p-6 text-sm">
                <div className="grid gap-3">
                  <div className="font-semibold">Current Compensation</div>
                  <ul className="grid gap-3">
                    <li className="flex items-center justify-between">
                      <span className="text-muted-foreground">Monthly Salary</span>
                      <span>₹{data.compensation.toFixed(2)}</span>
                    </li>
                  </ul>
                  <Separator className="my-2" />
                  <div className="font-semibold">Deductions</div>
                  <DeductionList deductions={data?.deductions} />
                  <Separator className="my-2" />
                  <ul className="grid gap-3">
                    <li className="flex items-center justify-between">
                      <span className="text-muted-foreground">Maximum Payable Amount</span>
                      <span>₹{data.compensation.toFixed(2)}</span>
                    </li>
                    <li className="flex items-center justify-between">
                      <span className="text-muted-foreground">Total Deductions</span>
                      <span>₹{data.amountDeducted.toFixed(2)}</span>
                    </li>
                    <li className="flex items-center justify-between font-semibold">
                      <span className="text-muted-foreground">Net Amount</span>
                      <span>₹{data.netAmount.toFixed(2)}</span>
                    </li>
                  </ul>
                </div>
                <div className="flex justify-end pt-10">
                  <Button onClick={onPayClick}>Confirm Payment</Button>
                </div>
              </CardContent>
            </Card>
          </div>
        )}
      </Container>
    </PageContainer>
  );
};

export default EstimatedSalary;
