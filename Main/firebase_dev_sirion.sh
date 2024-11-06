COLOR_TITLE='\e[1;33m'  # Amarillo brillante
COLOR_RESET='\e[0m'

# PULL
echo -e "${COLOR_TITLE}"
echo "podman pull 73197546/firebase_dev_op360_dev:1.0.1"
      podman pull 73197546/firebase_dev_op360_dev:1.0.1
echo -e "${COLOR_RESET}"

# RUN
echo -e "${COLOR_TITLE}"
echo "podman run -d -p 8181:80 73197546/firebase_dev_op360_dev:1.0.1"
      podman run -d -p 8181:80 73197546/firebase_dev_op360_dev:1.0.1
echo -e "${COLOR_RESET}"

# Finalizado
echo -e "${COLOR_TITLE}"
echo "finalizado"
echo -e "${COLOR_RESET}"
