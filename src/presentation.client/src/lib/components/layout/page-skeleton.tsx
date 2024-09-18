import React from 'react';
import { Skeleton } from 'xplorer-ui';

const getFormSkeleton = (rows: number) => {
  const arr = [...Array(Math.ceil(rows / 2)).keys()];

  return (
    <div className="flex flex-col space-y-3">
      {arr.map((x) => (
        <Skeleton key={x} className="h-8 " />
      ))}
      <div className="flex flex-row">
        <Skeleton className="h-[125px] rounded-xl w-1/2 mr-1" />
        <Skeleton className="h-[125px] rounded-xl w-1/2 ml-1" />
      </div>
      {arr.map((x) => (
        <Skeleton key={x} className="h-8 " />
      ))}
    </div>
  );
};

const getListSkeleton = (rows: number) => {
  return (
    <div className="flex flex-col space-y-3">
      {[...Array(rows).keys()].map((x) => (
        <Skeleton key={x} className="h-8 " />
      ))}
    </div>
  );
};

type SkeeletonProps = {
  isLoading: boolean;
  form?: boolean;
  rows?: number;
  children: React.ReactNode;
};

export function PageSkeleton({
  isLoading,
  form = false,
  rows = 5,
  children,
}: SkeeletonProps) {
  return (
    <>
      {isLoading && <>{form ? getFormSkeleton(rows) : getListSkeleton(rows)}</>}
      {!isLoading && <>{children}</>}
    </>
  );
}
