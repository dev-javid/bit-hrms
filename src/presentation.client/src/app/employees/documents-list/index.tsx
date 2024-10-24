import { ActionButton, BackButton, BreadCrumbProps, PageContainer, PageHeader } from '@/lib/components';
import { Card, CardContent, Container, useSimpleModal } from 'xplorer-ui';
import { DocumentType, Employee, EmployeeDocument } from '@/lib/types';
import DocumentForm from '../document-form';
import { useLoadData } from './useLoadData';

const DocumentsList = () => {
  const { employee, isLoading } = useLoadData();

  const breadCrumb: BreadCrumbProps = {
    title: 'Employees',
    to: './../../',
    state: {},
    child: {
      title: employee?.fullName || '',
      to: './../',
      child: {
        title: 'Documents',
        to: '',
      },
    },
  };

  return (
    <PageContainer breadCrumb={breadCrumb}>
      <PageHeader title="Documents">
        <BackButton to="./../../" text="Employees" />
      </PageHeader>
      <Container isLoading={isLoading}>{employee && <Dcouments employee={employee} />}</Container>
    </PageContainer>
  );
};

const Dcouments = ({ employee }: { employee: Employee }) => {
  const { showModal, hideModal } = useSimpleModal();
  const acceptedTypes: DocumentType[] = ['PAN', 'Aadhar'];

  const onAddDocument = (documentType: DocumentType) => {
    showModal(
      `Add ${documentType} (${employee!.fullName})`,
      <DocumentForm
        employee={employee!}
        document={{
          type: documentType,
          url: '',
          updatedOn: new Date(),
        }}
        onSuccess={() => hideModal()}
      />
    );
  };

  const onEditDocument = (document: EmployeeDocument) => {
    showModal(
      `Replace ${document.type} (${employee.fullName})`,
      <DocumentForm onSuccess={() => hideModal()} employee={employee} document={document} />
    );
  };

  return (
    <Card>
      <CardContent>
        <div className="flex justify-end py-2">
          <div className="container mx-auto px-4">
            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-3 gap-8">
              {acceptedTypes.map((acceptedType, index) => {
                const document = employee.documents.find((d) => d.type === acceptedType);
                return (
                  <Card key={index}>
                    <CardContent>
                      <div className="text-lg mb-2">{acceptedType}</div>
                      {document?.url ? (
                        <div
                          onClick={() => window.open(document!.url)}
                          key={index}
                          className="cursor-pointer aspect-square relative overflow-hidden bg-gray-200 rounded"
                        >
                          <img className="absolute inset-0 w-full h-full object-cover" src={document!.url} alt={acceptedType} loading="lazy" />
                        </div>
                      ) : (
                        <div className="text-gray-500">{acceptedType} not uploaded yet</div>
                      )}
                      <div className="flex justify-between mt-4">
                        <ActionButton
                          text={document?.url ? `Replace` : `Add`}
                          onClick={() => (document?.url ? onEditDocument(document) : onAddDocument(acceptedType))}
                        />
                        {document?.url && (
                          <div>
                            Updated on:
                            <p className="text-xs">{document?.updatedOn.toString().asDateOnly().toDayString()}</p>
                          </div>
                        )}
                      </div>
                    </CardContent>
                  </Card>
                );
              })}
            </div>
          </div>
        </div>
      </CardContent>
    </Card>
  );
};

export default DocumentsList;
