import { Container } from 'xplorer-ui';
import useDefaultValues from './use-default-values';
import FormContainer from './form-container';

const LeavePolicyForm = () => {
  const { defaultValues, isLoading } = useDefaultValues();
  return <Container isLoading={isLoading}>{!isLoading && <FormContainer defaultValues={defaultValues} />}</Container>;
};

export default LeavePolicyForm;
