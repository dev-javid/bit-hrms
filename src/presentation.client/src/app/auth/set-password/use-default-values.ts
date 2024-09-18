import { useParams, useSearchParams } from 'react-router-dom';
import { FormSchemaType } from './schema';

const useDefaultValues = () => {
  const [searchParams] = useSearchParams();
  const token = searchParams.get('token');
  const { userId } = useParams();
  const defaultValues: FormSchemaType = {
    token: token ?? '',
    userId: userId ?? '',
    password: '',
  };

  return {
    defaultValues,
  };
};

export default useDefaultValues;
