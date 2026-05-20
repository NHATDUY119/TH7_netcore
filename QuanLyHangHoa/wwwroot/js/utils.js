// ==================== Formatting ====================

function formatCurrency(amount) {
    return new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND'
    }).format(amount);
}

function formatNumber(num) {
    return new Intl.NumberFormat('vi-VN').format(num);
}

function formatDate(date) {
    return new Intl.DateTimeFormat('vi-VN').format(new Date(date));
}

// ==================== Alert & Notifications ====================

function showAlert(message, type = 'info') {
    const alertEl = document.getElementById('alert');
    alertEl.textContent = message;
    alertEl.className = `alert alert-${type}`;
    
    setTimeout(() => {
        alertEl.classList.add('alert-hidden');
    }, 3000);
}

// ==================== Validation ====================

function validateAddForm() {
    const tenHang = document.getElementById('tenHang').value.trim();
    const donViTinh = document.getElementById('donViTinh').value;
    const soLuongTon = document.getElementById('soLuongTon').value;
    const donGia = document.getElementById('donGia').value;
    const maNCC = document.getElementById('maNCC').value;

    if (!tenHang) {
        showAlert('Tên hàng không được để trống', 'error');
        return false;
    }

    if (!donViTinh) {
        showAlert('Đơn vị tính không được để trống', 'error');
        return false;
    }

    if (!soLuongTon || parseInt(soLuongTon) < 0) {
        showAlert('Số lượng tồn phải là số >= 0', 'error');
        return false;
    }

    if (!donGia || parseFloat(donGia) < 0) {
        showAlert('Đơn giá phải là số >= 0', 'error');
        return false;
    }

    if (!maNCC) {
        showAlert('Nhà cung cấp không được để trống', 'error');
        return false;
    }

    return true;
}

function validateEditForm() {
    const tenHang = document.getElementById('editTenHang').value.trim();
    const donViTinh = document.getElementById('editDonViTinh').value;
    const soLuongTon = document.getElementById('editSoLuongTon').value;
    const donGia = document.getElementById('editDonGia').value;
    const maNCC = document.getElementById('editMaNCC').value;

    if (!tenHang) {
        showAlert('Tên hàng không được để trống', 'error');
        return false;
    }

    if (!donViTinh) {
        showAlert('Đơn vị tính không được để trống', 'error');
        return false;
    }

    if (!soLuongTon || parseInt(soLuongTon) < 0) {
        showAlert('Số lượng tồn phải là số >= 0', 'error');
        return false;
    }

    if (!donGia || parseFloat(donGia) < 0) {
        showAlert('Đơn giá phải là số >= 0', 'error');
        return false;
    }

    if (!maNCC) {
        showAlert('Nhà cung cấp không được để trống', 'error');
        return false;
    }

    return true;
}

// ==================== Confirmation ====================

function confirmDelete(name) {
    return confirm(`Bạn có chắc chắn muốn xóa "${name}" không?`);
}

// ==================== DOM Helpers ====================

function hideLoading() {
    const loading = document.getElementById('listLoading');
    if (loading) {
        loading.style.display = 'none';
    }
}

function createTableRow(hangHoa, suppliers = []) {
    const supplierName = suppliers.find(s => s.maNCC === hangHoa.maNCC)?.tenNCC || 'N/A';
    
    return `
        <tr>
            <td>${hangHoa.maHang}</td>
            <td>${hangHoa.tenHang}</td>
            <td>${hangHoa.donViTinh}</td>
            <td>${formatNumber(hangHoa.soLuongTon)}</td>
            <td>${formatCurrency(hangHoa.donGia)}</td>
            <td>${supplierName}</td>
            <td>
                <button class="btn btn-warning" onclick="editHangHoa(${hangHoa.maHang})">Sửa</button>
                <button class="btn btn-danger" onclick="deleteHangHoaHandler(${hangHoa.maHang}, '${hangHoa.tenHang}')">Xóa</button>
            </td>
        </tr>
    `;
}

function createSupplierTableRow(supplier) {
    return `
        <tr>
            <td>${supplier.maNCC}</td>
            <td>${supplier.tenNCC}</td>
            <td>${supplier.diaChi || 'N/A'}</td>
            <td>${supplier.dienThoai || 'N/A'}</td>
        </tr>
    `;
}
