'use client'
import GenderButton from '@/component/Button/GenderButton'
import UserProfileHorizontal from '@/component/Card/UserProfileHorizontal'
import { usePostProfile } from '@/hook/myAccount'
import { UserGender } from '@/lib/enum/UserGender'
import IUser from '@/lib/interface/IMyAccount'
import { Button, Container, Heading, Input, VStack } from '@chakra-ui/react'
import { redirect, useRouter } from 'next/navigation'
import React, { ChangeEvent, FormEvent, MouseEvent, useEffect, useState } from 'react'

interface ProfileForm {
  name: string,
  surname: string,
  birth: string,
  gender: string,
  userId:string
}

const ProfileConfiguration = ({ user }: { user: IUser | null }) => {
  const {isLoading,post} = usePostProfile()
  if(user != null)
  {
      if(user.profile != undefined)
      {
        return redirect("/quickstart?num=2")
      }
  }
  const [form, setForm] = useState<ProfileForm>({ name: "", surname: "", birth: "", gender: "",userId:user?.userId ?? "" })
  const [gender, setGender] = useState("")
  const router = useRouter()


  useEffect(() => {
    if (user != null) {
      if (user.profile) {
        setForm(
          {
            name: user.profile?.name,
            surname: user.profile?.surname,
            birth: user.profile.birth,
            gender: user.profile.gender,
            userId: user.userId
          }
        )
      }
    }
  }, [])


  function handleSubmit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault()
    post(form)
    router.push("/quickstart?num=2")
  }

  function handleChange(e: ChangeEvent<HTMLInputElement>) {
    setForm({
      ...form,
      [e.target.name]: e.target.value
    });
  }


  function handleSelect(e: MouseEvent<HTMLButtonElement>) {
    if (e.currentTarget.id === UserGender.Male) {
      setGender(UserGender.Male)
      setForm({
        ...form,
        gender: UserGender.Male
      })
    }
    if (e.currentTarget.id === UserGender.Female) {
      setGender(UserGender.Female)
      setForm({
        ...form,
        gender: UserGender.Female
      })
    }
  }


  return (
    <VStack w="100%" spacing={8}>
      <Heading size="lg">
        Completa tu perfil
      </Heading>
      <VStack w="100%">
        <UserProfileHorizontal form={form} src={user?.picture ?? undefined} />
        <VStack w="100%">
          <Container w={["100%", "80%", "50%"]}>
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
                  autoComplete="true"
                  placeholder=""
                  borderColor="brand.100"
                  bg="white"
                  _focus={{ borderColor: "brand.100" }}
                />
              </VStack>
              <Button type="submit" bg="brand.100" color="white" variant='solid' w="50%" _hover={{ bg: "black" }}>
                {
                  isLoading ? "Enviando.." : 'Continuar'
                }
              </Button>
            </form>
          </Container>
        </VStack>
      </VStack>
    </VStack>
  )
}

export default ProfileConfiguration