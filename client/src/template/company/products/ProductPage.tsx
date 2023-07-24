'use client'
import { useCompanyLogged } from '@/hook/myCompany'
import { useProducts } from '@/hook/useProduct'
import IProduct from '@/lib/interface/IProduct'
import { Table, TableCaption, TableContainer, Tbody, Td, Tfoot, Th, Thead, Tr } from '@chakra-ui/react'
import React from 'react'

export const ProductPage = () => {
  const { company } = useCompanyLogged()
  const { products, isLoading } = useProducts(company ? company.companyId : undefined)

  return (
    <TableContainer>
      <Table variant='simple'>
        <TableCaption>Listado de productos</TableCaption>
        <Thead>
          <Tr>
            <Th>Codigo</Th>
            <Th>Nombre</Th>
          </Tr>
        </Thead>
        <Tbody>
          {
            products ?
              products.map((item,idx)=>{
                return(
                  <Tr key={idx}>
                    <Td>{item.code}</Td>
                    <Td>{item.name}</Td>
                  </Tr>
                )
              })
            :
            <>
            </>
          }
        </Tbody>
      </Table>
    </TableContainer>
  )
}
