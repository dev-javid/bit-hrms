import { Link } from 'react-router-dom';
import { Breadcrumb, BreadcrumbList, BreadcrumbItem, BreadcrumbLink, BreadcrumbSeparator } from 'xplorer-ui';

export interface BreadCrumbProps {
  title: string;
  to: string;
  state?: object;
  child?: BreadCrumbProps;
}

const getBreadcrumb = (breadCrumb: BreadCrumbProps): JSX.Element[] => {
  const elements: JSX.Element[] = [];

  elements.push(
    <BreadcrumbItem key={breadCrumb.title}>
      <BreadcrumbLink asChild>
        {breadCrumb.child ? (
          <Link to={breadCrumb.to} state={breadCrumb.state}>
            {breadCrumb.title}
          </Link>
        ) : (
          <span>{breadCrumb.title}</span>
        )}
      </BreadcrumbLink>
    </BreadcrumbItem>,
  );

  if (breadCrumb.child) {
    elements.push(<BreadcrumbSeparator key={`${breadCrumb.title}-separator`} />);
    elements.push(...getBreadcrumb(breadCrumb.child));
  }

  return elements;
};

const HeaderBreadcrumb = ({ breadCrumb }: { breadCrumb: BreadCrumbProps }) => {
  return (
    <Breadcrumb className="flex">
      <BreadcrumbList>{getBreadcrumb(breadCrumb ?? { title: 'Bit Xplorer', to: '/' })}</BreadcrumbList>
    </Breadcrumb>
  );
};

export default HeaderBreadcrumb;
