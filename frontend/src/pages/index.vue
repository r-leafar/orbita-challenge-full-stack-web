<template>
  <AppMenuBar>
    <slot>
      <v-form class="py-12" ref="formRef" @submit.prevent="submitForm">
        <v-container>
          <v-card class="mx-auto pa-12 pb-8 mt-4" elevation="8" max-width="448" rounded="lg">
            <div class="text-subtitle-1 text-medium-emphasis">Email</div>

            <v-text-field v-model="values.email" :rules="[...rules.email]" density="compact" placeholder="Email"
              prepend-inner-icon="mdi-email-outline" variant="outlined"></v-text-field>

            <div class="text-subtitle-1 text-medium-emphasis d-flex align-center justify-space-between">
              Senha

              <a class="text-caption text-decoration-none text-blue" href="#" rel="noopener noreferrer" target="_blank">
                Esqueceu a senha?</a>
            </div>

            <v-text-field :rules="[...rules.password]" v-model="values.password"
              :append-inner-icon="visible ? 'mdi-eye-off' : 'mdi-eye'" :type="visible ? 'text' : 'password'"
              density="compact" placeholder="Digite sua senha" prepend-inner-icon="mdi-lock-outline" variant="outlined"
              @click:append-inner="visible = !visible"></v-text-field>

            <v-btn type="submit" class="mb-8" color="blue" size="large" variant="tonal" block>
              Log In
            </v-btn>

            <v-card-text class="text-center">
              <a class="text-blue text-decoration-none" href="#" rel="noopener noreferrer" target="_blank">
                Sign up now <v-icon icon="mdi-chevron-right"></v-icon>
              </a>
            </v-card-text>
          </v-card>
        </v-container>
      </v-form>
    </slot>
  </AppMenuBar>
</template>

<script lang="ts" setup>
import { ref } from 'vue'
import { useForm } from '@/composables/useForm'
import { useAuth } from '@/composables/useAuth'

const { login } = useAuth()

const { values, formRef, rules, validate } = useForm(
  { email: '', password: '' },
  {
    email: [
      (v: string) => !!v || 'Obrigatório',
      (v: string) => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(v) || 'E-mail inválido'
    ],
    password: [(v: string) => !!v || 'Obrigatório']
  }
)

const visible = ref(false)

const submitForm = async () => {
  const { valid } = await validate()
  if (valid) {
    login()
  } else {
    console.warn('❌ Formulário inválido')
  }
}

</script>