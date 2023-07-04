'use client'
import GenderButton from '@/component/Button/GenderButton'
import UserProfileHorizontal from '@/component/Card/UserProfileHorizontal'
import { Button, Container, HStack, Input, VStack } from '@chakra-ui/react'
import { useRouter } from 'next/navigation'
import React, { ChangeEvent, FormEvent, MouseEvent, useState } from 'react'

interface IintialForm {
  name: string,
  surname: string,
  birth?: string,
  gender: string
}

const intialForm: IintialForm = {
  name: "",
  surname: "",
  birth: undefined,
  gender: ""
}
enum Gender {
  Male = "0",
  Female = "1"
}



const ProfileConfiguration = () => {
  const [form, setForm] = useState(intialForm)
  const [gender, setGender] = useState("")
  const router = useRouter()

  function handleSubmit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault()
    console.log(form)
    router.push("/quickstart?num=2")
  }

  function handleChange(e: ChangeEvent<HTMLInputElement>) {
    setForm({
      ...form,
      [e.target.name]: e.target.value
    });
  }


  function handleSelect(e: MouseEvent<HTMLButtonElement>) {
    if (e.currentTarget.id === Gender.Male) {
      setGender(Gender.Male)
      setForm({
        ...form,
        gender: "Masculino"
      })
    }
    if (e.currentTarget.id === Gender.Female) {
      setGender(Gender.Female)
      setForm({
        ...form,
        gender: "Femenino"
      })
    }
  }


  return (
    <VStack w="100%" spacing={8}>
      <UserProfileHorizontal form={form} />
      <VStack w="100%">
        <Container w={["100%","80%","50%"]}>
          <form onSubmit={(e) => handleSubmit(e)} style={{ width: "100%", display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", gap: "2rem" }}>
            <VStack w="100%">
              <Input
                name='name'
                defaultValue={form.name}
                onChange={(e) => handleChange(e)}
                variant='filled'
                type="text"
                autoCapitalize="true"
                autoComplete="name"
                placeholder='Nombre'
                borderColor="brand.100"
                bg="white"
                _focus={{ borderColor: "brand.100" }}
              />
              <Input
                name='surname'
                defaultValue={form.surname}
                onChange={(e) => handleChange(e)}
                variant='filled'
                type="text"
                autoCapitalize="true"
                autoComplete="surname"
                placeholder='Apellido'
                borderColor="brand.100"
                bg="white"
                _focus={{ borderColor: "brand.100" }}
              />
              <GenderButton handleSelect={handleSelect} selectedGender={gender} />
              <Input
                name='birth'
                onChange={(e) => handleChange(e)}
                variant='filled'
                type="date"
                autoCapitalize="true"
                autoComplete="birth"
                placeholder='Apellido'
                borderColor="brand.100"
                bg="white"
                _focus={{ borderColor: "brand.100" }}
              />
            </VStack>
            <Button type="submit" bg="brand.100" color="white" variant='solid' w="50%" _hover={{ bg: "black" }}>
              Continuar
            </Button>
          </form>
        </Container>

      </VStack>
    </VStack>
  )
}

export default ProfileConfiguration