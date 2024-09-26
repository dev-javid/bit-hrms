import { BreadCrumbProps, PageContainer, PageHeader, PageSkeleton } from '@/lib/components';
import { useColumns } from './useColumns';
import { Company } from '@/lib/types';
import { CardContent, Card, Button, ClientSideDataTable, useSimpleModal, useSimpleConfirm, toast } from 'xplorer-ui';
import { useDeleteCompanyMutation, useGetCompaniesQuery } from '@/lib/rtk/rtk.comapnies';
import CompanyForm from '../company-form';

const CompanyList = () => {
  const { hideModal, showModal } = useSimpleModal();
  const { showConfirm } = useSimpleConfirm();
  const { data, isLoading, isFetching } = useGetCompaniesQuery(null);
  const [deleteCompany] = useDeleteCompanyMutation();

  const onEditClick = (company: Company) => {
    showModal('Edit Company', <CompanyForm company={company} onSuccess={hideModal} />);
  };

  const onAddNewClick = () => {
    showModal('Add New Company', <CompanyForm onSuccess={hideModal} />);
  };

  const onDeleteClick = async (company: Company) => {
    const ok = await showConfirm(
      'Delete Company',
      <div>
        Are you sure you want to delete the company - <strong>{company.name}</strong>?
      </div>
    );
    if (ok) {
      const response = await deleteCompany(company.companyId);

      if (!('error' in response)) {
        toast({
          title: 'Success ',
          description: 'Company deleted',
        });
      }
    }
  };

  const breadCrumb: BreadCrumbProps = {
    title: 'Companies',
    to: './',
  };

  const columns = useColumns(onEditClick, onDeleteClick);

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title="Companies">
        <Button variant="outline" onClick={onAddNewClick}>
          Add company
        </Button>
      </PageHeader>
      <PageSkeleton isLoading={isLoading || isFetching}>
        <Card>
          <CardContent>{data && <ClientSideDataTable data={data?.items} columns={columns} />}</CardContent>
        </Card>
      </PageSkeleton>
    </PageContainer>
  );
};

export default CompanyList;
