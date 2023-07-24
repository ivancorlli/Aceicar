import CompanyType from '@/component/Card/CompanyType'
import IType from '@/lib/interface/IType'
import ConvertTypes from '@/utils/ConverTypes'
import { Button, Grid, HStack, VStack, useToast } from '@chakra-ui/react'
import { useRouter } from 'next/navigation'
import React, { useState, MouseEvent } from 'react'
import { useQuery } from 'react-query'


async function getTypes():Promise<IType[]> {
  try {
    const res = await fetch("/api/types")
    const data = await res.json()
    if (data == null || data == undefined) return []
    return data
  } catch (e: any) {
    return []
  }
}


const StepOne = ({onSelect}:{onSelect:(typeId:string)=>void}) => {
  const [selected, setSelect] = useState("")
  const toast = useToast()
  const router = useRouter()
  const { data } = useQuery({ queryKey: "company-types", queryFn: getTypes })

  function handleClick(id: string) {
    setSelect(id)
    onSelect(id)
  }

  function handleNext(e: MouseEvent<HTMLButtonElement>) {
    if (selected != "") {
      router.push("/create-a-company?step=2")
    } else {
      toast({
        title: "Debe seleccionar un tipo de negocio",
        status: "error",
        position: "top",
        isClosable: true,
      })
    }
  }

  return (
    <VStack w="100%" spacing={6}>
      <Grid templateColumns="repeat(2,1fr)" gap={8} w="100%">
        {
          data ?
          data.length > 0 ?
          data.map((type:IType,idx) => {
            return <CompanyType key={idx} icon={type.icon ?? undefined} name={ConvertTypes(type.name)}  id={type.typeId} onSelect={(id: string) => handleClick(id)} selected={selected} />
          })
          : <></>
          :
          <>
          </>
        }
      </Grid>
      <HStack justifyContent="end" w="100%">
        <Button
          onClick={(e) => handleNext(e)}
          _hover={{
            bg: "black"
          }}
          _active={{
            bg: "black"
          }}
          bg="brand.100"
          color="white"
        >
          Siguiente
        </Button>
      </HStack>
    </VStack>
  )
}

export default StepOne