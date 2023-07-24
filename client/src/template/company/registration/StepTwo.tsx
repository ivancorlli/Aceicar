import CompanyType from '@/component/Card/CompanyType'
import ISpecialization from '@/lib/interface/ISpecialization'
import ConvertSpecializations from '@/utils/ConvertSpecialization'
import { Button, Grid, HStack, VStack, useToast } from '@chakra-ui/react'
import { useRouter } from 'next/navigation'
import React, { useState,MouseEvent, useEffect } from 'react'
import { useQuery } from 'react-query'

async function getSpecialization(typeId:string):Promise<ISpecialization[]> {
    try {
      const res = await fetch(`/api/types/${typeId}/specialization`)
      const data = await res.json()
      if (data == null || data == undefined) return []
      return data
    } catch (e: any) {
      return []
    }
  }
  
  interface IForm {
    typeId?:string,
    specializationId?:string
  }
  

const StepTwo = ({form,onSelect,onBack}:{form:IForm,onSelect:(specializationId:string)=>void,onBack:()=>void}) => {
    const [selected, setSelect] = useState("")
    const toast = useToast()
    const router = useRouter()
    const { data } = useQuery(["company-specializations",form.typeId], ()=>getSpecialization(form.typeId!),{enabled:!!form.typeId})


    useEffect(()=>{
        if(!form.typeId)
        {
            router.push("/create-a-company?step=1")
        }
    },[])

    function handleClick(id: string) {
        setSelect(id)
        onSelect(id)
    }

    function handleNext(e: MouseEvent<HTMLButtonElement>) {
        if (selected != "") {
            router.push("/create-a-company?step=3")
        } else {
            toast({
                title: "Debe seleccionar una especialidad",
                status: "error",
                position: "top",
                isClosable: true,
            })
        }
    }

    function handleBack(e:MouseEvent<HTMLButtonElement>)
    {
        setSelect("")
        onBack()
        router.push("/create-a-company?step=1")
    }

    return (
        <VStack w="100%" spacing={6}>
            <Grid templateColumns="repeat(2,1fr)" gap={8} w="100%">
            {
          data ?
          data.length > 0 ?
          data.map((type:ISpecialization,idx) => {
            return <CompanyType key={idx} icon={type.icon ?? undefined} name={ConvertSpecializations(type.name)}  id={type.specializationId} onSelect={(id: string) => handleClick(id)} selected={selected} />
          })
          : <></>
          :
          <>
          </>
        }
            </Grid>
            <HStack justifyContent="space-between" w="100%">
            <Button
                    onClick={(e) => handleBack(e)}
                    _hover={{
                        bg: "black"
                    }}
                    _active={{
                        bg: "black"
                    }}
                    bg="brand.100"
                    color="white"
                >
                    Atras
                </Button>
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

export default StepTwo