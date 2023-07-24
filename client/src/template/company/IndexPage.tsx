'use client'
import ICompanyLogged from '@/lib/interface/ICompanyLogged'
import { redirect } from 'next/navigation'
import React from 'react'
import { useQuery } from 'react-query'

interface IFetcherError {
  ok: boolean,
  redirect: boolean
}
async function getCompanyLogged(): Promise<ICompanyLogged | IFetcherError | null> {
  try {
      const res = await fetch("/api/access/company/logged")
      const data = await res.json()
      if (data == null || data == undefined) return null
      return data
  } catch (e: any) {
      return null
  }
}

const IndexPage = () => {
  const query = useQuery({ queryKey: "company-logged", queryFn: getCompanyLogged })
    const toRed: IFetcherError | null = query.data as IFetcherError | null ?? null
    const company :ICompanyLogged | null = query.data as ICompanyLogged | null ?? null
    if(company)
    {
      if(company.contactData == undefined) return redirect("/dashboard/quickstart")
      if(company.location == undefined) return redirect("/dashboard/quickstart?step=2")
    }
  return (
    <div>Company Dashboradr</div>
  )
}

export default IndexPage