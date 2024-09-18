import {
  BreadCrumbProps,
  PageContainer,
  PageHeader,
  PageSkeleton,
} from '@/lib/components';
import { columns } from './columns';
import { Company } from '@/lib/types';
import { Link, useNavigate } from 'react-router-dom';
import { CardContent, Card, Button, ClientSideDataTable } from 'xplorer-ui';
import { useGetCompaniesQuery } from '@/lib/rtk/rtk.comapnies';

const CompanyList = () => {
  const navigate = useNavigate();
  const { data, isLoading, isFetching } = useGetCompaniesQuery(null);

  const onEdit = (data: Company) => {
    navigate(`${data.companyId}`, { state: data });
  };

  const breadCrumb: BreadCrumbProps = {
    title: 'Companies',
    to: './',
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title="Employees">
        <Link to="companies/0">
          <Button>Add new</Button>
        </Link>
      </PageHeader>
      <PageSkeleton isLoading={isLoading || isFetching}>
        <Card>
          <CardContent>
            {data && (
              <ClientSideDataTable
                data={data?.items}
                columns={columns}
                onEdit={onEdit}
              />
            )}
          </CardContent>
        </Card>
      </PageSkeleton>
    </PageContainer>
  );
};

export default CompanyList;
