const PageHeader = ({ title, children }: { title: string; children?: React.ReactNode | React.ReactNode[] }) => {
  return (
    <div className="flex items-center gap-4 px-2 pb-4">
      <h1 className="flex-1 shrink-0 whitespace-nowrap text-xl font-semibold tracking-tight sm:grow-0">{title}</h1>
      <div className="items-center gap-2 md:ml-auto md:flex">{children}</div>
    </div>
  );
};

export default PageHeader;
